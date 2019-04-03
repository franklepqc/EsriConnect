using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ESRI.NetCore.Interfaces.Tests
{
    [TestClass]
    public class ClientCopierTests
    {
        /// <summary>
        /// Client pour appeler les services REST.
        /// </summary>
        private static IClient _client;

        /// <summary>
        /// Initialiser le client.
        /// </summary>
        /// <param name="testContext">Contexte de test.</param>
        [ClassInitialize]
        public static void InitialiserClient(TestContext testContext)
        {
            _client = new ServiceCollection()
                .AddScoped<IConstructeurUrl, ConstructeurUrl>()
                .AddScoped<IParametresRequete, ParametresRequete>()
                .AddScoped<IRepoFeatureClass, RepoFeatureClass>()
                .AddScoped<IClient, Client>()
                .BuildServiceProvider()
                .GetService<IClient>();
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client - Copier features")]
        public void CopierAvecCriteres()
        {
            // Variables de travail.
            // À la place de localhost, entrez votre url.
            var urlBaseSource = @"https://localhost/agsweb/rest/services/TravauxPublic/PlaNidPoule/FeatureServer/0";
            var urlBaseDestination = @"https://localhost/agsweb/rest/services/TravauxPublic/PlaNidPoule/FeatureServer/1";
            string where = "1 = 1";

            // Attendu.
            var attendu = true;

            // Actuel.
            var actuel = _client.Copier<ObjetFeature>(urlBaseSource, urlBaseDestination, where);

            // Assert.
            Assert.AreEqual(attendu, actuel);
        }

        /// <summary>
        /// Classe pour les ndps.
        /// </summary>
        public class ObjetFeature
        {
            public int OBJECTID { get; set; }
            public int NoRequete { get; set; }
            public string Gabarit { get; set; }
            public string Sujet { get; set; }
            public string Statut { get; set; }
            public string Localisation { get; set; }
            public string Description { get; set; }
            public int Artere { get; set; }
            public int Zone { get; set; }
            public string Equipe { get; set; }
            public string Secteur { get; set; }
            public long DateEnregistrement { get; set; }
            public string HeureEnregistrement { get; set; }
            public int NbJourEcoule { get; set; }
            public int Source { get; set; }
            public int EtatRequete { get; set; }
            public int NbNdp { get; set; }
            public int Completer { get; set; }
            public string Remarque { get; set; }
        }
    }
}
