pageextension 50100 CustomerCardExtension extends "Customer Card"
{
    layout
    {
        addafter(Name)
        {
            field(FavoriteBook; Rec.FavoriteBook)
            {
                ApplicationArea = All;
                Visible = true;
            }
        }
    }
}