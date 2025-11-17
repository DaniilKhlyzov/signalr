using System;
using System.Linq;
using BadNews.Models.Comments;
using BadNews.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsRepository commentsRepository;

        public CommentsController(CommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        [HttpGet("api/news/{newsId}/comments")]
        public ActionResult<CommentsDto> GetCommentsForNews(Guid newsId)
        {
            var comments = commentsRepository.GetComments(newsId);

            var dto = new CommentsDto
            {
                NewsId = newsId,
                Comments = comments
                    .Select(comment => new CommentDto
                    {
                        User = comment.User,
                        Value = comment.Value
                    })
                    .ToArray()
            };

            return Ok(dto);
        }
    }
}


