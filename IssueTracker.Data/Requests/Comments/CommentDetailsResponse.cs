using System.Collections.Generic;

namespace IssueTracker.Data.Requests.Comments
{
    public class CommentDetailsResponse
    {
        public CommentDetailsResponse()
        {
            Changes = new List<CommentChange>();
        }

        public Comment Comment { get; set; }
        public Person Poster { get; set; }
        public List<CommentChange> Changes { get; set; }

    }
}
