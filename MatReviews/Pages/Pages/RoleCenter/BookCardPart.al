page 50104 BookCardPart
{
    PageType = CardPart;
    ApplicationArea = All;
    UsageCategory = Administration;
    SourceTable = Book;

    layout
    {
        area(Content)
        {

            cuegroup(BookStats)
            {

                ShowCaption = false;
                field(NumberOfBooks; Rec.NumberOfBooks)
                {
                    Caption = 'Number of books';
                    ApplicationArea = All;
                    DrillDownPageID = BookList;
                }

                field(HardCoverBooks; Rec.HardCoverBooks)
                {
                    Caption = 'Hardcover books';
                    ApplicationArea = All;
                    DrillDownPageID = BookList;
                }

                field(SumOfHoursToRead; Rec.SumOfHoursToRead)
                {
                    Caption = 'Sum of hours to read';
                    ApplicationArea = All;
                    DrillDownPageID = BookList;
                }

            }

        }
    }
}