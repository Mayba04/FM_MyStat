using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Tests
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int MaxPoint {  get; set; }
        public IEnumerable<TestStudentAnswer> TestStudentAnswers {  get; set; }
    }
}
