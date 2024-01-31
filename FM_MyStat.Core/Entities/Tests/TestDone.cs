using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Tests
{
    public class TestDone : IEntity
    {
        public int Id { get; set; }
        public int StudentId {  get; set; }
        public Student Student { get; set; }
        public int TestId {  get; set; }
        public Test Test { get; set; }
        public int Point {  get; set; }
        public IEnumerable<TestStudentAnswer> TestStudentAnswers { get; set; }
    }
}
