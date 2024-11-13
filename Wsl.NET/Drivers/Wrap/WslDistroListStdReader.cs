using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Wsl.NET.IPC;

namespace Wsl.NET.Drivers.Wrap
{
    /// <summary>
    /// 
    /// </summary>
    public class WslDistroListStdReader : IStdResultReader<IEnumerable<WslDistro>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stdin"></param>
        /// <param name="stderr"></param>
        /// <param name="exitCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<ProcessCommandResult<IEnumerable<WslDistro>>> ReadAsync(
            StreamReader stdin, 
            StreamReader stderr, 
            int exitCode, 
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            return ReadAsyncInternal(
                stdin,
                exitCode,
                cancellationToken
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stdin"></param>
        /// <param name="exitCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<ProcessCommandResult<IEnumerable<WslDistro>>> ReadAsyncInternal(
            StreamReader stdin,
            int exitCode,
            CancellationToken cancellationToken
        )
        {
            string result = 
                await stdin
                    .ReadToEndAsUTF8Async(
                        cancellationToken
                    );

            string error = 
                string.Empty;

            if (exitCode != 0)
            {
                error = result;

                return new ProcessCommandResult<IEnumerable<WslDistro>>(                    
                    Enumerable.Empty<WslDistro>(),
                    result,
                    error, 
                    exitCode
                );
            }

            IEnumerable<WslDistro> distros = 
                await ParseAsync(
                    result, 
                    cancellationToken
                );

            return new ProcessCommandResult<IEnumerable<WslDistro>>(
                distros,
                result,
                error,
                exitCode
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stdout"></param>
        /// <returns></returns>
        private static string RemoveHeaders(string stdout)
        {
            return
                stdout[(stdout.IndexOf(Environment.NewLine) + Environment.NewLine.Length)..];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="exitCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private static Task<IEnumerable<WslDistro>> ParseAsync(
            string value,
            CancellationToken cancellationToken
        )
        {
            string result =
                RemoveHeaders(value);

            var matches =
                Regex.Matches(
                    result,
                    @"(\*|\s{1,2})[\s|](.+[^\s])[\s]+([Stopped]{7}|[Running]{7})[\s]+([\d]+)"
                );

            return Task.FromResult(
                (IEnumerable<WslDistro>)(
                    ParseMatches(matches, cancellationToken)
                        .ToList()
                )
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matches"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static IEnumerable<WslDistro> ParseMatches(
            MatchCollection matches,
            CancellationToken token
        )
        {
            token.ThrowIfCancellationRequested();

            foreach (Match match in matches)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                WslDistro distro =
                    new WslDistro
                    (
                        match.Groups[2].Value.Trim(),
                        (WslDistroState)Enum.Parse(
                            typeof(WslDistroState),
                            match.Groups[3].Value
                        ),
                        (WslDistroVersion)int.Parse(match.Groups[4].Value),
                        match.Groups[1].Value == "*"
                    );

                yield return distro;
            }
        }
    }
}
