public partial class FileResponse : System.IDisposable
{
    private  IDisposable? _client;
    private System.IDisposable? _response;

    public int StatusCode { get; private set; }

    public  IReadOnlyDictionary<string,  IEnumerable<string>> Headers { get; private set; }

    public Stream Stream { get; private set; }

    public bool IsPartial
    {
        get { return StatusCode == 206; }
    }

    public FileResponse(int statusCode, IReadOnlyDictionary<string, IEnumerable<string>> headers, Stream stream, IDisposable? client, System.IDisposable? response)
    {
        StatusCode = statusCode;
        Headers = headers;
        Stream = stream;
        _client = client;
        _response = response;
    }

    public void Dispose()
    {
        Stream.Dispose();
        if (_response != null)
            _response.Dispose();
        if (_client != null)
            _client.Dispose();
    }
}