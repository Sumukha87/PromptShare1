using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PromptShare.Models
{
    public class Prompt
    {

        public int Id { get; set; }
        public string PromptHead { get; set; }
        public string PromptDes { get; set; }

        public Prompt()
        {
            
        }

    }
}
