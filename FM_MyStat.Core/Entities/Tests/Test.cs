using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities.Tests
{
    public class Test : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxPoint {  get; set; }
        public string Description { get; set; }
        public int SubjectId {  get; set; }
        public Subject Subject { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<TestDone> TestDones { get; set; }
    }
}
