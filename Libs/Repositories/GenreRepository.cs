﻿using Libs.Contracts;
using Libs.Data;
using Libs.Models;
using Libs.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public class GenreRepository : RepositoryBase<Actor>, IGenreRepository
    {
        public GenreRepository(ApplicationDbContext dBContext) : base(dBContext) { }

        public void Delete(Genre data)
        {
            _dbContext.Remove(data);
        }

        public List<Genre> getAll()
        {
            return _dbContext.Genres.ToList();
        }

        public IEnumerable<Actor> GetAll(Expression<Func<User, bool>> filter = null, Func<IQueryable<Actor>, IOrderedQueryable<Actor>> oderBy = null, int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }

        public void Insert(Genre data)
        {
            _dbContext.Genres.Add(data);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Genre data)
        {
            _dbContext.Genres.Update(data);
        }
        public Genre GetById(object id)
        {
            Genre data = _dbContext.Genres.Find(id);
            if (data != null)
                return data;
            throw new Exception("Genre not found");
        }
    }
}