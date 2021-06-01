tableextension 50100 CustomerTableExtension extends Customer
{
    fields
    {
        field(50100; FavoriteBook; Integer)
        {
            TableRelation = Book.Id;
            DataClassification = ToBeClassified;
        }
    }

}