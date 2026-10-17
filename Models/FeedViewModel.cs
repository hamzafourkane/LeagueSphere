using System.Collections.Generic;

namespace LeagueSphere.Models
{
    public class FeedViewModel
    {
        public List<Post> Posts { get; set; } = new();
        public List<Match> MatchsRecents { get; set; } = new();
        public List<Match> MatchsAVenir { get; set; } = new();
    }
}