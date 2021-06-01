page 50106 RoleCenterBookCardPart
{
    PageType = CardPart;
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
                    DrillDownPageId = BookList;
                }

                field(NumOfHardcoverBooks; Rec.NumOfHardcoverBooks)
                {
                    Caption = 'Number of hardcover books';
                    ApplicationArea = All;
                    DrillDownPageId = BookList;
                }

                field(SumOfHoursToRead; Rec.SumOfHoursToRead)
                {
                    Caption = 'Hours to read all books';
                    ApplicationArea = All;
                }
            }
        }
    }

}