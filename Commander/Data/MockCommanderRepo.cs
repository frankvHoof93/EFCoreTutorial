using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        private List<Command> commands = new List<Command>{
                new Command{
                    Id=0,
                    HowTo="Boil an egg",
                    Line="Boil Water",
                    Platform="Kettle & Pan"
                },
                new Command{
                    Id=1,
                    HowTo="Cut Bread",
                    Line="Get a knife",
                    Platform="Knife & Chopping Board"
                },
                new Command{
                    Id=2,
                    HowTo="Make cup of tea",
                    Line="Place teabag in cup",
                    Platform="Kettle & Cup"
                }
        };

        public void CreateCommand(Command cmd)
        {
            commands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{
                Id=id,
                HowTo="Boil an egg",
                Line="Boil Water",
                Platform="Kettle & Pan"
            };
        }

        public bool SaveChanges()
        {
            return true;
        }

        public void UpdateCommand(Command cmd)
        {
        }
    }
}