page 50100 BookCard
{
    PageType = Card;
    ApplicationArea = All;
    UsageCategory = Administration; // For searching the page
    SourceTable = Book;
    DelayedInsert = true;   // Az ablak bezárásakor mentsen
    AutoSplitKey = true;    // Automatikus ID generálás

    layout
    {
        area(Content)
        {
            group(General)
            {
                field(Title; Rec.Title)
                {
                    ApplicationArea = All;
                    ShowMandatory = true;   // The red * beside the field only visuals
                }

                field(Author; Rec.AuthorId)
                {
                    ApplicationArea = All;
                }
            }

            group(Details)
            {
                field(PageCount; Rec.PageCount)
                {
                    ApplicationArea = All;
                }

                field(HardCover; Rec.HardCover)
                {
                    ApplicationArea = All;
                }
            }

        }
    }

}