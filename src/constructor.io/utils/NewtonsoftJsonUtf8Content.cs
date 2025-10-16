using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Constructorio_NET.Utils;

internal sealed class NewtonsoftJsonUtf8Content : HttpContent
{
    private static readonly MediaTypeHeaderValue Utf8JsonMediaTypeHeaderValue = new("application/json");
    private static readonly UTF8Encoding Utf8EncodingWithoutByteOrderMark = new(encoderShouldEmitUTF8Identifier: false);
    internal static readonly JsonSerializer DefaultJsonSerializer = JsonSerializer.CreateDefault();
    private readonly MemoryStream _memoryStream = new();
    private bool _disposed;

    public NewtonsoftJsonUtf8Content(in object body)
    {
        // The server requires the content length, so we must serialize it up front, to be able to tell the length

        // https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions.defaultbuffersize
        const int systemTextJsonJsonSerializerOptionsDefaultBufferSize = 16384;

        // using will flush the writer when disposing
        using StreamWriter streamWriter = new(_memoryStream, encoding: Utf8EncodingWithoutByteOrderMark, leaveOpen: true, bufferSize: systemTextJsonJsonSerializerOptionsDefaultBufferSize /* needs bufferSize to have leaveOpen... */);
        DefaultJsonSerializer.Serialize(streamWriter, body);

        Headers.ContentType = Utf8JsonMediaTypeHeaderValue;
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposing || _disposed)
            return;
        _disposed = true;
        _memoryStream.Dispose();
        base.Dispose(true);
    }

    protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
    {
        _memoryStream.Seek(0, SeekOrigin.Begin);
        await _memoryStream.CopyToAsync(stream);
    }

    protected override bool TryComputeLength(out long length)
    {
        length = _memoryStream.Length;
        return true;
    }
}