using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using SogetiDemo.TDD.BankApp.Common.Utils;

namespace SogetiDemo.TDD.BankApp.Repository
{
    public abstract class RavenDbRepositoryBase<T> : IDisposable
    {
        private const string RavenDbUrl = "http://localhost:8080";
        
        protected readonly IDocumentSession DocumentSession;
       
        protected RavenDbRepositoryBase(IDocumentSession session)
        {
            Assert.NotNull(session, "session");

            DocumentSession = session;
        }

        protected RavenDbRepositoryBase()
        {
            DocumentSession = CreateSession();
        }

        /// <summary>
        /// Saves a entity
        /// </summary>
        /// <param name="entity">the entity</param>
        public void Save(T entity)
        {
            DocumentSession.Store(entity);
            DocumentSession.SaveChanges();
        }


        /// <summary>
        /// Removes a entity
        /// </summary>
        /// <param name="entity">the entity</param>
        public void Remove(T entity)
        {
            DocumentSession.Delete(entity);
            DocumentSession.SaveChanges();
        }


        /// <summary>
        /// Get an entity by its id
        /// </summary>
        /// <returns>the entity, if found</returns>
        public T GetById(dynamic id)
        {
            return DocumentSession.Load<T>(id);
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>All entities</returns>
        public ICollection<T> GetAll()
        {
            return DocumentSession.Query<T>().ToList();
        }

        /// <summary>
        /// Disposes the DocumentSession
        /// </summary>
        public void Dispose()
        {
            if (DocumentSession != null)
            {
                DocumentSession.Dispose();
            }
        }

        protected abstract string SessionName { get; }

        /// <summary>
        /// Creates the document session
        /// </summary>
        /// <returns></returns>
        private IDocumentSession CreateSession()
        {
            IDocumentStore documentStore = new DocumentStore { Url = RavenDbUrl };
            return documentStore.Initialize().OpenSession(SessionName);
        }
    }
}