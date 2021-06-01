pageextension 50101 CustomerListExtension extends "Customer List"
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