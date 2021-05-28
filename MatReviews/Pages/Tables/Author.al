table 50101 Author
{
    DataClassification = ToBeClassified;

    fields
    {
        field(1; Id; Integer)
        {
            DataClassification = ToBeClassified;

        }

        field(2; Name; Text[150])
        {
            DataClassification = ToBeClassified;
        }

        field(3; NumberOfBooksWritten; Integer)
        {
            FieldClass = FlowField;
            // select count(*) from Book where AuthorId = Author.Id
            CalcFormula = count(Book Where(AuthorId = field(Id)));
        }
    }

    keys
    {
        key(PK; Id)
        {
            Clustered = true;
        }
    }

    fieldgroups
    {
        fieldgroup(DropDown; Id, Name, NumberOfBooksWritten)    // What you see when the table is accessed through a dropdown
        {

        }
    }
}