page 50101 BookList
{
    PageType = List;
    ApplicationArea = All;
    UsageCategory = Administration;
    SourceTable = Book;
    CardPageId = BookCard;
    Caption = 'Books';

    layout
    {
        area(Content)
        {
            repeater(Books)
            {
                field(Id; Rec.Id)
                {
                    ApplicationArea = All;
                }

                field(Title; Rec.Title)
                {
                    ApplicationArea = All;
                }

                field(PageCount; Rec.PageCount)
                {
                    ApplicationArea = All;
                }

                field(HoursToRead; Rec.HoursToRead)
                {
                    ApplicationArea = All;
                    Caption = 'Hours to read';
                }

                field(AuthorId; Rec.AuthorId)
                {
                    Caption = 'Author Id';
                    ApplicationArea = All;
                }

                field(AuthorName; Rec.AuthorName)
                {
                    Caption = 'Author Name';
                    ApplicationArea = All;
                }
            }
        }

        area(FactBoxes)
        {
            systempart(MyLinks; Links)
            {
                ApplicationArea = All;
            }

            systempart(MyNotes; Notes)
            {
                ApplicationArea = All;
            }
        }
    }

    actions
    {
        area(Navigation)
        {
            action(AuthorList)
            {
                ApplicationArea = All;
                Caption = 'To Author List';
                RunObject = Page AuthorList;
                RunPageMode = View;
            }
        }

        area(Reporting)
        {
            action(CreateReport)
            {
                ApplicationArea = All;
                Caption = 'Create Report';
                RunObject = Page AuthorList;
                Image = Camera;
            }
        }

        area(Processing)
        {
            action(Process)
            {
                ApplicationArea = All;
                Caption = 'Create Report';
                RunObject = Page AuthorList;
            }
        }
    }

    trigger OnOpenPage();
    var
        MyCodeUnit: Codeunit MyTestCodeUnit;
    begin
        MyCodeUnit.BooksMessage();
    end;

}