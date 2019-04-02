using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ESRI.NetCore.Interfaces.Tests
{
    [TestClass]
    public class ClientAjouterTests
    {
        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client - Ajouter features")]
        public void AjouterFeatures1Seul()
        {
            // Variables de travail.
            var urlBase = @"http://localhost";
            var mockRepo = new Mock<IRepoFeatureClass>();
            var nombreEnregistrements = 0;

            // Préparer l'enregistrement.
            mockRepo.Setup(k => k.Ajouter(It.IsAny<string>(), It.IsAny<IEnumerable<ObjetFeature>>()))
                .Callback<string, IEnumerable<ObjetFeature>>((url, elements) =>
                {
                    nombreEnregistrements += elements.Count();
                });

            var provider = new ServiceCollection()
                .AddTransient<IConstructeurUrl, ConstructeurUrl>()
                .AddTransient<IParametresRequete, ParametresRequete>()
                .AddScoped((serviceProvider) => mockRepo.Object)
                .AddScoped<IClient, Client>()
                .BuildServiceProvider();

            var client = provider.GetService<IClient>();

            // Attendu.
            var attendu = true;

            // Actuel.
            var actuel = client.AjouterFeatures(urlBase, new List<ObjetFeature>() { new ObjetFeature() });

            // Assert.
            Assert.AreEqual(attendu, actuel);
            Assert.AreEqual(1, nombreEnregistrements);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client - Ajouter features")]
        public void AjouterFeatures117()
        {
            // Variables de travail.
            var urlBase = @"http://localhost";
            var mockRepo = new Mock<IRepoFeatureClass>();
            var nombreEnregistrements = 0;
            var listeElements = new List<ObjetFeature>();

            // Préparer l'enregistrement.
            mockRepo.Setup(k => k.Ajouter(It.IsAny<string>(), It.IsAny<IEnumerable<ObjetFeature>>()))
                .Callback<string, IEnumerable<ObjetFeature>>((url, elements) =>
                {
                    nombreEnregistrements += elements.Count();
                });

            for (int i = 0; i < 117; i++)
            {
                listeElements.Add(new ObjetFeature());
            }

            var provider = new ServiceCollection()
                .AddTransient<IConstructeurUrl, ConstructeurUrl>()
                .AddTransient<IParametresRequete, ParametresRequete>()
                .AddScoped((serviceProvider) => mockRepo.Object)
                .AddScoped<IClient, Client>()
                .BuildServiceProvider();

            var client = provider.GetService<IClient>();

            // Attendu.
            var attendu = true;

            // Actuel.
            var actuel = client.AjouterFeatures(urlBase, listeElements);

            // Assert.
            Assert.AreEqual(attendu, actuel);
            Assert.AreEqual(117, nombreEnregistrements);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client - Ajouter features")]
        public void AjouterFeatures50()
        {
            // Variables de travail.
            var urlBase = @"http://localhost";
            var mockRepo = new Mock<IRepoFeatureClass>();
            var nombreEnregistrements = 0;
            var listeElements = new List<ObjetFeature>();

            // Préparer l'enregistrement.
            mockRepo.Setup(k => k.Ajouter(It.IsAny<string>(), It.IsAny<IEnumerable<ObjetFeature>>()))
                .Callback<string, IEnumerable<ObjetFeature>>((url, elements) =>
                {
                    nombreEnregistrements += elements.Count();
                });

            for (int i = 0; i < 50; i++)
            {
                listeElements.Add(new ObjetFeature());
            }

            var provider = new ServiceCollection()
                .AddTransient<IConstructeurUrl, ConstructeurUrl>()
                .AddTransient<IParametresRequete, ParametresRequete>()
                .AddScoped((serviceProvider) => mockRepo.Object)
                .AddScoped<IClient, Client>()
                .BuildServiceProvider();

            var client = provider.GetService<IClient>();

            // Attendu.
            var attendu = true;

            // Actuel.
            var actuel = client.AjouterFeatures(urlBase, listeElements);

            // Assert.
            Assert.AreEqual(attendu, actuel);
            Assert.AreEqual(50, nombreEnregistrements);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client - Ajouter features")]
        public void AjouterFeatures100()
        {
            // Variables de travail.
            var urlBase = @"http://localhost";
            var mockRepo = new Mock<IRepoFeatureClass>();
            var nombreEnregistrements = 0;
            var listeElements = new List<ObjetFeature>();

            // Préparer l'enregistrement.
            mockRepo.Setup(k => k.Ajouter(It.IsAny<string>(), It.IsAny<IEnumerable<ObjetFeature>>()))
                .Callback<string, IEnumerable<ObjetFeature>>((url, elements) =>
                {
                    nombreEnregistrements += elements.Count();
                });

            for (int i = 0; i < 100; i++)
            {
                listeElements.Add(new ObjetFeature());
            }

            var provider = new ServiceCollection()
                .AddTransient<IConstructeurUrl, ConstructeurUrl>()
                .AddTransient<IParametresRequete, ParametresRequete>()
                .AddScoped((serviceProvider) => mockRepo.Object)
                .AddScoped<IClient, Client>()
                .BuildServiceProvider();

            var client = provider.GetService<IClient>();

            // Attendu.
            var attendu = true;

            // Actuel.
            var actuel = client.AjouterFeatures(urlBase, listeElements);

            // Assert.
            Assert.AreEqual(attendu, actuel);
            Assert.AreEqual(100, nombreEnregistrements);
        }

        /// <summary>
        /// Classe de test.
        /// </summary>
        class ObjetFeature
        {

        }
    }
}
