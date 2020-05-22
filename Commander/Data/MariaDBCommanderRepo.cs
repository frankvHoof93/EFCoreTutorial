using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Contexts;
using Commander.Models;

namespace Commander.Data
{
    public class MariaDBCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public MariaDBCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Commands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException(nameof(cmd));

            _context.Commands.Remove(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(c => c.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateCommand(Command cmd)
        {
            //Nothing. This is handled by Controller by updating the original object (See CommandsController.UpdateCommand)
        }
    }
}