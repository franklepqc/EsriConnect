using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ESRI.NetCore.Interfaces.Tests
{
    [TestClass]
    public class ClientModifierTests
    {
        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client - Modifier features")]
        public void ModifierFeatures1Seul()
        {
            // Variables de travail.
            var urlBase = @"http://localhost";
            var mockRepo = new Mock<IRepoFeatureClass>();
            var nombreEnregistrements = 0;

            // Préparer l'enregistrement.
            mockRepo.Setup(k => k.MettreAJour(It.IsAny<string>(), It.IsAny<IEnumerable<IFeatureSet<ObjetFeature>>>()))
                .Callback<string, IEnumerable<IFeatureSet<ObjetFeature>>>((url, elements) =>
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
            var actuel = client.ModifierFeatures(urlBase, new List<FeatureSet<ObjetFeature>>() { new FeatureSet<ObjetFeature>() { attributes = new ObjetFeature() }});

            // Assert.
            Assert.AreEqual(attendu, actuel);
            Assert.AreEqual(1, nombreEnregistrements);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client - Modifier features")]
        public void ModifierFeatures117()
        {
            // Variables de travail.
            var urlBase = @"http://localhost";
            var mockRepo = new Mock<IRepoFeatureClass>();
            var nombreEnregistrements = 0;
            var listeElements = new List<FeatureSet<ObjetFeature>>();

            // Préparer l'enregistrement.
            mockRepo.Setup(k => k.MettreAJour(It.IsAny<string>(), It.IsAny<IEnumerable<IFeatureSet<ObjetFeature>>>()))
                .Callback<string, IEnumerable<IFeatureSet<ObjetFeature>>>((url, elements) =>
                {
                    nombreEnregistrements += elements.Count();
                });

            for (int i = 0; i < 117; i++)
            {
                listeElements.Add(new FeatureSet<ObjetFeature>() { attributes = new ObjetFeature() });
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
            var actuel = client.ModifierFeatures(urlBase, listeElements);

            // Assert.
            Assert.AreEqual(attendu, actuel);
            Assert.AreEqual(117, nombreEnregistrements);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client - Modifier features")]
        public void ModifierFeatures50()
        {
            // Variables de travail.
            var urlBase = @"http://localhost";
            var mockRepo = new Mock<IRepoFeatureClass>();
            var nombreEnregistrements = 0;
            var listeElements = new List<FeatureSet<ObjetFeature>>();

            // Préparer l'enregistrement.
            mockRepo.Setup(k => k.MettreAJour(It.IsAny<string>(), It.IsAny<IEnumerable<IFeatureSet<ObjetFeature>>>()))
                .Callback<string, IEnumerable<IFeatureSet<ObjetFeature>>>((url, elements) =>
                {
                    nombreEnregistrements += elements.Count();
                });

            for (int i = 0; i < 50; i++)
            {
                listeElements.Add(new FeatureSet<ObjetFeature>() { attributes = new ObjetFeature() });
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
            var actuel = client.ModifierFeatures(urlBase, listeElements);

            // Assert.
            Assert.AreEqual(attendu, actuel);
            Assert.AreEqual(50, nombreEnregistrements);
        }

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"Client - Modifier features")]
        public void ModifierFeatures100()
        {
            // Variables de travail.
            var urlBase = @"http://localhost";
            var mockRepo = new Mock<IRepoFeatureClass>();
            var nombreEnregistrements = 0;
            var listeElements = new List<FeatureSet<ObjetFeature>>();

            // Préparer l'enregistrement.
            mockRepo.Setup(k => k.MettreAJour(It.IsAny<string>(), It.IsAny<IEnumerable<IFeatureSet<ObjetFeature>>>()))
                .Callback<string, IEnumerable<IFeatureSet<ObjetFeature>>>((url, elements) =>
                {
                    nombreEnregistrements += elements.Count();
                });

            for (int i = 0; i < 100; i++)
            {
                listeElements.Add(new FeatureSet<ObjetFeature>() { attributes = new ObjetFeature() });
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
            var actuel = client.ModifierFeatures(urlBase, listeElements);

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
