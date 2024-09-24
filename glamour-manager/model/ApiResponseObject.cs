
public class ApiResponseObject
{
    public required ApiResponsePagination Pagination { get; set; }
    public required List<FfxivItem> Results { get; set; }
}