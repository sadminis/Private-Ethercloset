public class ApiResponsePagination
{
    public int? PageNext { get; set; }
    public int? PagePrev { get; set; }
    public int PageTotal { get; set; }
    public int Results { get; set; }
    public int ResultsPerPage { get; set; }
    public int ResultsTotal { get; set; }
}