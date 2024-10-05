using IntelChat.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Task = IntelChat.Models.Task;

namespace IntelChat.Services
{
	public class EntityService
	{
		public List<Answer> answers { get; set; } = new List<Answer>();
		public List<Brand> brands { get; set; } = new List<Brand>();
		public List<Guide> guides { get; set; } = new List<Guide>();
		public List<Interview> interviews { get; set; } = new List<Interview>();
		public List<Location> locations { get; set; } = new List<Location>();
		public List<Memo> memos { get; set; } = new List<Memo>();
		public List<Noun> nouns { get; set; } = new List<Noun>();
		public List<Nova> novas { get; set; } = new List<Nova>();
		public List<Person> persons { get; set; } = new List<Person>();
		public List<Pod> pods { get; set; } = new List<Pod>();
		public List<Pype> pypes { get; set; } = new List<Pype>();
		public List<Question> questions { get; set; } = new List<Question>();
		public List<Registration> registrations { get; set; } = new List<Registration>();
		public List<Task> tasks { get; set; } = new List<Task>();
		public List<Url> urls { get; set; } = new List<Url>();
		public List<Verb> verbs { get; set; } = new List<Verb>();
		public List<Work> works { get; set; } = new List<Work>();
	}
}
