using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Tests
{
    public class Answer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public bool Correct {  get; set; } = false;
        public IEnumerable<TestStudentAnswer> TestStudentAnswers { get; set; }
    }
}
