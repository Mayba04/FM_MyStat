using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Tests
{
    public class TestStudentAnswer : IEntity
    {
        public int Id { get; set; }
        public int TestDoneId {  get; set; }
        public TestDone TestDone { get; set; }
        public int QuestionId {  get; set; }
        public Question Question { get; set; }
        public int AnswerId {  get; set; }
        public Answer Answer { get; set; }
        public bool Correct { get; set; }
        public int Point {  get; set; }
    }
}
