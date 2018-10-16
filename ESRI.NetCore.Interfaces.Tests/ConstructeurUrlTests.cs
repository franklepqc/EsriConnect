using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ESRI.NetCore.Interfaces.Tests
{
    /// <summary>
    /// Tests pour la construction des tests.
    /// </summary>
    [TestClass]
    public class ConstructeurUrlTests
    {
        /// <summary>
        /// Injection de dépendances.
        /// </summary>
        private IServiceProvider _serviceCollection = new ServiceCollection()
            .AddScoped<IConstructeurUrl, ConstructeurUrl>()
            .BuildServiceProvider();

        /// <summary>
        /// Test primaire de construction.
        /// </summary>
        [TestMethod]
        [TestCategory(@"ConstructeurUrl")]
        public void Construire()
        {
            // Variables de travail.
            var constructeur = _serviceCollection.GetService<IConstructeurUrl>();
            var urlBase = @"https://sdgis.ville.gatineau.qc.ca/agsweb/rest/services/General/DistrictCanu/MapServer/2";

            // Attendu.
            var attendu = new Uri(urlBase);

            // Actuel.
            constructeur.Debuter(urlBase);
            var actuel = constructeur.Finaliser();

            // Assert.
            Assert.AreEqual(attendu, actuel);
        }

        /// <summary>
        /// Test primaire de construction avec des caractères spéciaux.
        /// </summary>
        [TestMethod]
        [TestCategory(@"ConstructeurUrl")]
        public void ConstruireAvecCaracteresSpeciaux()
        {
            // Variables de travail.
            var constructeur = _serviceCollection.GetService<IConstructeurUrl>();
            var urlBase = @"https://sdgis.ville.gatineau.qc.ca/@%22";

            // Attendu.
            var attendu = new Uri(urlBase);

            // Actuel.
            constructeur.Debuter(urlBase);
            var actuel = constructeur.Finaliser();

            // Assert.
            Assert.AreEqual(attendu, actuel);
        }

        /// <summary>
        /// Ajout d'une condition.
        /// </summary>
        [TestMethod]
        [TestCategory(@"ConstructeurUrl")]
        public void ConstruireAvecClauseWhere()
        {
            // Variables de travail.
            var constructeur = _serviceCollection.GetService<IConstructeurUrl>();
            var urlBase = @"https://sdgis.ville.gatineau.qc.ca/agsweb/rest/services/General/DistrictCanu/MapServer/2";
            var whereClause = @"1 = 1";
            var parametres = new Mock<IParametresRequete>();

            // Assigner paramètres.
            parametres.SetupGet(k => k.Where).Returns(whereClause);
                
            // Attendu.
            var attendu = new Uri(urlBase + @"/query?where=1+%3D+1&returnGeometry=false&f=json");

            // Actuel.
            constructeur.Debuter(urlBase);
            constructeur.AjouterParametres(parametres.Object);
            var actuel = constructeur.Finaliser();

            // Assert.
            Assert.AreEqual(attendu, actuel);
        }

        /// <summary>
        /// Ajout d'une condition.
        /// </summary>
        [TestMethod]
        [TestCategory(@"ConstructeurUrl")]
        public void ConstruireAvecPoint()
        {
            // Variables de travail.
            var constructeur = _serviceCollection.GetService<IConstructeurUrl>();
            var urlBase = @"https://sdgis.ville.gatineau.qc.ca/agsweb/rest/services/General/DistrictCanu/MapServer/2";
            var point = new Point
            {
                X = 42187d,
                Y = 55748d,
                SRID = 32189
            };
            var clauseWhere = @"1 = 1";
            var parametres = new Mock<IParametresRequete>();

            // Assigner paramètres.
            parametres.SetupGet(k => k.Where).Returns(clauseWhere);
            parametres.SetupGet(k => k.Point).Returns(point);

            // Attendu.
            var attendu = new Uri(urlBase + @"/query?where=1+%3D+1&geometry=42187%2C55748&geometryType=esriGeometryPoint&inSR=32189&returnGeometry=false&f=json");

            // Actuel.
            constructeur.Debuter(urlBase);
            constructeur.AjouterParametres(parametres.Object);
            var actuel = constructeur.Finaliser();

            // Assert.
            Assert.AreEqual(attendu, actuel);
        }

        /// <summary>
        /// Ajout d'une condition.
        /// </summary>
        [TestMethod]
        [TestCategory(@"ConstructeurUrl")]
        public void ConstruireAvecPointAvecDecimales()
        {
            // Variables de travail.
            var constructeur = _serviceCollection.GetService<IConstructeurUrl>();
            var urlBase = @"https://sdgis.ville.gatineau.qc.ca/agsweb/rest/services/General/DistrictCanu/MapServer/2";
            var point = new Point
            {
                X = 42187.123123d,
                Y = 55748.456456d,
                SRID = 32189
            };
            var clauseWhere = @"1 = 1";
            var parametres = new Mock<IParametresRequete>();

            // Assigner paramètres.
            parametres.SetupGet(k => k.Where).Returns(clauseWhere);
            parametres.SetupGet(k => k.Point).Returns(point);

            // Attendu.
            var attendu = new Uri(urlBase + @"/query?where=1+%3D+1&geometry=42187.123123%2C55748.456456&geometryType=esriGeometryPoint&inSR=32189&returnGeometry=false&f=json");

            // Actuel.
            constructeur.Debuter(urlBase);
            constructeur.AjouterParametres(parametres.Object);
            var actuel = constructeur.Finaliser();

            // Assert.
            Assert.AreEqual(attendu, actuel);
        }

        /// <summary>
        /// Ajout d'une condition.
        /// </summary>
        [TestMethod]
        [TestCategory(@"ConstructeurUrl")]
        public void ConstruireAvecClauseWhereEtPoint()
        {
            // Variables de travail.
            var constructeur = _serviceCollection.GetService<IConstructeurUrl>();
            var urlBase = @"https://sdgis.ville.gatineau.qc.ca/agsweb/rest/services/General/DistrictCanu/MapServer/2";
            var whereClause = @"1 = 1";
            var point = new Point
            {
                X = 42187d,
                Y = 55748d,
                SRID = 32189
            };
            var parametres = new Mock<IParametresRequete>();

            // Assigner paramètres.
            parametres.SetupGet(k => k.Where).Returns(whereClause);
            parametres.SetupGet(k => k.Point).Returns(point);

            // Attendu.
            var attendu = new Uri(urlBase + @"/query?where=1+%3D+1&geometry=42187%2C55748&geometryType=esriGeometryPoint&inSR=32189&returnGeometry=false&f=json");

            // Actuel.
            constructeur.Debuter(urlBase);
            constructeur.AjouterParametres(parametres.Object);
            var actuel = constructeur.Finaliser();

            // Assert.
            Assert.AreEqual(attendu, actuel);
        }

        /// <summary>
        /// Construire avec une Url invalide.
        /// </summary>
        [TestMethod]
        [TestCategory(@"ConstructeurUrl")]
        [ExpectedException(typeof(UriFormatException))]
        public void ConstruireAvecUrlInvalide()
        {
            // Variables de travail.
            var constructeur = _serviceCollection.GetService<IConstructeurUrl>();
            var urlBase = @"toto";

            // Actuel.
            constructeur.Debuter(urlBase);
            constructeur.Finaliser();
        }
    }
}
