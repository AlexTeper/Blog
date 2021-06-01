page 50100 BookCard
{
    PageType = Card;
    ApplicationArea = All;
    UsageCategory = Administration;
    SourceTable = Book;
    AutoSplitKey = true;
    DelayedInsert = true;

    layout
    {
        area(Content)
        {
            group(General)
            {
                field(Title; Rec.Title)
                {
                    ApplicationArea = All;
                    //NotBlank = true;
                    ShowMandatory = true;
                }

                field(PageCount; Rec.PageCount)
                {
                    ApplicationArea = All;
                }

                field(Author; Rec.AuthorId)
                {
                    ApplicationArea = All;
                }

                field(HardCover; Rec.HardCover)
                {
                    Caption = 'Hard Cover';
                    ApplicationArea = All;
                }
            }
        }
    }
}