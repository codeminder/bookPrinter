using System;

namespace bookPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Please enter count pages in PDF: ");
            int countPages = Convert.ToInt32(Console.ReadLine());
            int wholeBookList = countPages / 4 + (countPages % 4 > 0 ? 1 : 0);
            Console.WriteLine("Lists: " + wholeBookList);
            Console.Write("Enter portion size (pages): ");
            int portionSize = Convert.ToInt32(Console.ReadLine());

            BookPaper[] wholeBook = new BookPaper[wholeBookList];

            for (int i = 0; i < wholeBookList; i++) {

                wholeBook[i] = new BookPaper(((i * 2 + 1) > countPages) ? 0 : (i * 2 + 1),
                ((i * 2 + 2) > countPages) ? 0 : (i * 2 + 2),
                ((wholeBookList * 4 - i * 2 - 1) > countPages) ? 0 : (wholeBookList * 4 - i * 2 - 1),
                ((wholeBookList * 4 - i * 2) > countPages ? 0 : wholeBookList * 4 - i * 2), 
                i + 1);

            }

            if (portionSize == 0 | portionSize > wholeBookList) {
                portionSize = wholeBookList;
            }

            string firstPagePacket = "";
            string secondPagePacket = "";
            int currentPacketSize = 0;
            int numberPocket = 1;

            for (int currentPosition = 0; currentPosition < wholeBookList; currentPosition ++) {

                if (currentPacketSize >= portionSize) {
                    Console.WriteLine($"--Portion-- {numberPocket}");
                    Console.WriteLine($"First side: {firstPagePacket}");
                    Console.WriteLine($"Second side: {secondPagePacket}");
                    Console.WriteLine("");
                    firstPagePacket = "";
                    secondPagePacket = "";
                    currentPacketSize = 0;
                    numberPocket ++;
                }
                currentPacketSize ++;
                firstPagePacket += (String.IsNullOrEmpty(firstPagePacket) ? "" : ", ")
                    + wholeBook[currentPosition].GetFirstSidePair();
                secondPagePacket = wholeBook[currentPosition].GetSecondSidePair() 
                    + (String.IsNullOrEmpty(secondPagePacket) ? "" : ", ")
                    + secondPagePacket;

            }

            if (!String.IsNullOrEmpty(firstPagePacket)) {
                Console.WriteLine($"--Portion-- {numberPocket}");
                Console.WriteLine($"First side: {firstPagePacket}");
                Console.WriteLine($"Second side: {secondPagePacket}");
                Console.WriteLine("");
            }            

        }
    }

    class BookPaper {
        private int number;
        private int firstPageNumber;
        private int secondPageNumber;
        private int thirdPageNumber;
        private int forthPageNumbers;

        public BookPaper()
        {
        }

        public BookPaper(int firstPageNumber, int secondPageNumber, int thirdPageNumber, int forthPageNumber, int number)
        {
            this.firstPageNumber = firstPageNumber;
            this.secondPageNumber = secondPageNumber;
            this.thirdPageNumber = thirdPageNumber;
            this.forthPageNumbers = forthPageNumber;
            this.number           = number;
        }

        public string GetFirstSidePair() {
            return Convert.ToString(forthPageNumbers) + ", " + Convert.ToString(firstPageNumber);
        }
        public string GetSecondSidePair() {
            return Convert.ToString(secondPageNumber) + ", " + Convert.ToString(thirdPageNumber);
        }

    }
}
