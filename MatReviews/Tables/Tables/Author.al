table 50101 Author
{
    DataClassification = ToBeClassified;
    // DrillDownPageId = BookList;
    // LookupPageId = BookCard;

    fields
    {
        field(1; Id; Integer)
        {
            DataClassification = ToBeClassified;

        }

        field(2; Name; Text[100])
        {
            DataClassification = ToBeClassified;
            trigger OnLookup();
            begin
                Message('LookUp trigger has run');
            end;
        }

        field(3; NumberOfBooksWritten; Integer)
        {
            Caption = 'Number of books written';
            Editable = false;
            FieldClass = FlowField;
            CalcFormula = count(Book Where(AuthorId = field(Id)));
        }
    }


    keys
    {
        key(Key1; Id)
        {
            Clustered = true;
        }
    }

    fieldgroups
    {
        fieldgroup(DropDown; Id, Name, NumberOfBooksWritten)
        {

        }
    }

}