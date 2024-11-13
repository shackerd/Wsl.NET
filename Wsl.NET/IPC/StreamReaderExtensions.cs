using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
    public static class StreamReaderExtensions
    {
        public static Task<string> ReadToEndAsUTF8Async(
            this StreamReader reader, 
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return ReadToEndAsUTF8AsyncInternal(
                reader, 
                cancellationToken
            );
        }

        private async static Task<string> ReadToEndAsUTF8AsyncInternal(
            StreamReader reader,
            CancellationToken cancellationToken
        )
        {
            string u16 =
                 await reader
                     .ReadToEndAsync();

            // stdout is utf16 but StreamReader on Process class is utf8
            // we need to remove unused bytes in string (this primitive type is UTF16 btw...)

            StringBuilder builder =
                new StringBuilder();

            for (int i = 0; i < u16.Length; i += 2)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                builder.Append(
                    u16[i]
                );
            }

            string u8 =
                builder.ToString();

            return u8;
        }
    }
}
