﻿using ApplicationCore.DataTransformation;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ApplicationCore.Services
{
    public class MenuService : IMenuService<MenuItem>
    {
        private readonly IRepository<MenuItem> _repository;

        public int Count => _repository.Count;

        public MenuService(IRepository<MenuItem> repository) => _repository = repository;

        public void AddNewItem(MenuItem item) => _repository.Add(item);

        public MenuItem ChangeItem(MenuItem item) => _repository.Update(item);

        public void DeleteItem(int id) => _repository.Delete(id);

        public MenuItem GetItem(int id) => _repository.GetById(id);

        public List<MenuItem> ListAllItems() => _repository.ListAll();

        public List<MenuItem> ListAllItems(int index, int count, MenuItem searchItem = null)
        {
            if (searchItem != null)
            {
                var expressionChecker = Expressions.GenerateComparisonExpressions(searchItem);
                return _repository.ListAll(index, count, expressionChecker);
            }
            return _repository.ListAll(index, count);
        }



        public List<MenuItem> ListAllItems(int index, int count, string orderColumn, string orderType, MenuItem searchItem = null)
        {
            if (searchItem != null)
            {
                var expressionChecker = Expressions.GenerateComparisonExpressions(searchItem);
                return _repository.ListAll(index, count, orderColumn, orderType, expressionChecker);

            }

            return _repository.ListAll(index, count, orderColumn, orderType);
        }

        public List<MenuItem> Find(Func<MenuItem, bool> rules) => _repository.Find(rules);
    }
}
