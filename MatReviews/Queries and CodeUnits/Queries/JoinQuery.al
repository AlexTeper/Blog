query 50100 JoinQuery
{
    QueryType = Normal;
    OrderBy = descending(PageCount);

    elements
    {
        dataitem(Author; Author)
        {
            column(AuthorName; Name)
            {

            }

            /*filter(FilterName; SourceFieldName)
            {
                
            }*/

            dataitem(Book; Book)
            {
                DataItemLink = AuthorId = Author.Id;
                SqlJoinType = InnerJoin;


                column(BookTitle; Title)
                {
                    // ColumnFilter = BookTitle = const('Harry Potter');
                }


                column(PageCount; PageCount)
                {
                    // Method = Sum;
                }
            }
        }
    }

    trigger OnBeforeOpen();
    begin
        SetFilter(PageCount, '>0');
        // TopNumberOfRows(1);
    end;
}