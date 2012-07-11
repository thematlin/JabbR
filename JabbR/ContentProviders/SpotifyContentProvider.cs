using JabbR.ContentProviders.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JabbR.ContentProviders
{
    public class SpotifyContentProvider : CollapsibleContentProvider
    {
        private readonly IEnumerable<string> _validUrls = new HashSet<string>
                                                             {
                                                                 "http://open.spotify.com/track/",
                                                                 "http://open.spotify.com/user/",
                                                                 "http://open.spotify.com/album/"
                                                             };

        protected override Task<ContentProviderResult> GetCollapsibleContent(ContentProviderHttpRequest request)
        {
            var spotifyUri = ExtractSpotifyUri(request.RequestUri.AbsolutePath);

            return TaskAsyncHelper.FromResult(new ContentProviderResult()
                                                  {
                                                      Content = string.Format("<iframe src=\"https://embed.spotify.com/?uri=spotify:{0}\" width=\"300\" height=\"380\" frameborder=\"0\" allowtransparency=\"true\"></iframe>", spotifyUri),
                                                      Title = string.Format("spotify:{0}", spotifyUri)
                                                  });
        }

        private string ExtractSpotifyUri(string requestUrl)
        {
            return requestUrl.Remove(0, 1).Replace('/', ':');
        }

        public override bool IsValidContent(Uri uri)
        {
            return _validUrls.Any(x => uri.AbsoluteUri.StartsWith(x));
        }
    }
}