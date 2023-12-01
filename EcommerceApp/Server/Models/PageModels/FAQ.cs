using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Server.Models.PageModels
{
    public class FAQ
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public virtual ICollection<FAQQuestion> Questions { get; set; } = new List<FAQQuestion>();
        public virtual ICollection<FAQAnswer> Answers { get; set; } = new List<FAQAnswer>();
        public DateTime DateModified { get; set; }
    }
    public class FAQAnswer
    {
        [Key]
        public Guid Id { get; set; }
        public string AnswerText { get; set; } = string.Empty;
        public Guid FAQId { get; set; }
        public virtual FAQ? FAQ { get; set; }
    }
    public class FAQQuestion
    {
        [Key]
        public Guid Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public Guid FAQId { get; set; }
        public virtual FAQ? FAQ { get; set; }
    }
}
