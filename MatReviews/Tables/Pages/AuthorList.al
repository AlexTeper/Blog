page 50102 AuthorList
{
    PageType = List;
    ApplicationArea = All;
    UsageCategory = Administration;
    SourceTable = Author;
    Editable = false;

    layout
    {
        area(Content)
        {
            repeater(General)
            {
                field(Id; Rec.Id)
                {
                    ApplicationArea = All;
                }

                field(Name; Rec.Name)
                {
                    ApplicationArea = All;
                }

                field(NumberOfBooksWritten; Rec.NumberOfBooksWritten)
                {
                    Caption = 'Number of books written';
                    ApplicationArea = All;
                }
            }
        }
    }
}