# Application de Gestion de Flotte de Véhicules 

## Description 

Cette application console en C# permet de gérer une flotte de véhicules, les chauffeurs, les trajets, la maintenance et fournit des statistiques. L'application propose un assistant interactif en console qui vous permet d'effectuer diverses actions telles que l'ajout, la modification, la suppression et l'affichage des véhicules, des chauffeurs, des trajets et des maintenances.
Les données sont persistées dans une base de données **SQLite**  à l'aide d'**Entity Framework Core** , garantissant ainsi que les informations sont sauvegardées entre les exécutions de l'application.
## Fonctionnalités 
 
- **Gestion des véhicules**  :
  - Ajouter, modifier, supprimer des véhicules de type Voiture, Camion ou Moto.

  - Définir le kilométrage initial lors de l'ajout ou de la modification d'un véhicule.

  - Afficher la liste des véhicules avec leurs détails.
 
- **Gestion des chauffeurs**  :
  - Ajouter, modifier, supprimer des chauffeurs avec leurs permis (A, B, C) et ancienneté.

  - Afficher la liste des chauffeurs avec leurs détails.
 
- **Gestion des trajets**  :
  - Créer des trajets en assignant un chauffeur et un véhicule approprié.

  - Le kilométrage du véhicule est mis à jour automatiquement après chaque trajet.

  - Afficher la liste des trajets effectués avec leurs détails.
 
- **Maintenance des véhicules**  :
  - Enregistrer des maintenances pour les véhicules avec le type de maintenance et le coût.

  - Recevoir des alertes lorsque la maintenance d'un véhicule est due en fonction du kilométrage.

  - Afficher l'historique des maintenances.
 
- **Statistiques**  :
  - Afficher des statistiques sur le total des kilomètres parcourus.

  - Identifier le chauffeur le plus actif en termes de kilomètres parcourus.

## Prérequis 

- .NET Core SDK (version 7.0 ou ultérieure) installé sur votre machine.

- SQLite est utilisé pour la base de données.
 
- **Entity Framework Core 7.0.0**  est utilisé comme ORM pour la gestion de la base de données.

## Installation 
 
1. **Cloner le dépôt ou télécharger les fichiers du projet :** 

```bash
git clone https://votre-repo.git
```
*ou* téléchargez les fichiers directement.
 
2. **Ouvrir le projet :** 
  - Avec un IDE comme Visual Studio, Visual Studio Code ou Rider.
 
  - Assurez-vous que tous les fichiers `.cs` sont inclus dans le projet.
 
3. **Configurer la base de données :** 
Avant d'exécuter l'application, vous devez appliquer les migrations à la base de données SQLite.
 
  - Si vous utilisez **Visual Studio** , exécutez les commandes suivantes dans la **Console du Gestionnaire de Paquets**  :

```bash
Add-Migration InitialCreate
Update-Database
```
 
  - Si vous utilisez la **.NET CLI** , exécutez les commandes suivantes dans votre terminal :

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
Ces commandes vont créer la base de données `flotte.db` dans le répertoire du projet, ainsi que les tables nécessaires pour les véhicules, chauffeurs et trajets.

## Exécution 

### Via un IDE 

1. Ouvrez le projet dans votre IDE.

2. Compilez le projet pour vérifier qu'il n'y a pas d'erreurs.

3. Exécutez le projet. La console s'ouvrira avec le menu interactif.

### Via la ligne de commande 
 
1. Ouvrez un terminal et naviguez jusqu'au répertoire du projet.


```bash
cd chemin/vers/le/projet
```
 
2. Exécutez la commande suivante pour exécuter l'application :


```bash
dotnet run
```

## Utilisation 

Lorsque vous exécutez l'application, un menu interactif s'affiche :


```markdown
=== Gestion de Flotte de Véhicules ===
1. Gestion des véhicules
2. Gestion des chauffeurs
3. Gestion des trajets
4. Maintenance des véhicules
5. Afficher les statistiques
6. Quitter
Veuillez choisir une option :
```

### Navigation dans les menus 
 
- **Gestion des véhicules :**  
  - **Ajouter un véhicule :**  Fournissez le type, l'immatriculation, la marque, le modèle et le kilométrage initial.
 
  - **Modifier un véhicule :**  Modifiez la marque, le modèle ou le kilométrage d'un véhicule existant.
 
  - **Supprimer un véhicule :**  Supprimez un véhicule en fournissant son immatriculation.
 
  - **Afficher tous les véhicules :**  Affiche la liste complète des véhicules enregistrés.
 
- **Gestion des chauffeurs :**  
  - **Ajouter un chauffeur :**  Fournissez le nom, le type de permis et l'ancienneté.
 
  - **Modifier un chauffeur :**  Modifiez le type de permis et l'ancienneté d'un chauffeur existant.
 
  - **Supprimer un chauffeur :**  Supprimez un chauffeur en fournissant son nom.
 
  - **Afficher tous les chauffeurs :**  Affiche la liste complète des chauffeurs enregistrés.
 
- **Gestion des trajets :**  
  - **Ajouter un trajet :**  Fournissez les informations du trajet (lieu de départ, lieu d'arrivée, distance, durée), ainsi que le nom du chauffeur et l'immatriculation du véhicule. 
    - **Note :**  Le kilométrage du véhicule sera automatiquement mis à jour après le trajet.
 
  - **Afficher tous les trajets :**  Affiche la liste des trajets effectués.
**Important :**  Vous devez avoir au moins un chauffeur et un véhicule enregistrés pour accéder à ce menu.
 
- **Maintenance des véhicules :**  
  - **Effectuer une maintenance :**  Enregistrez une maintenance pour un véhicule en fournissant son immatriculation, le type de maintenance et le coût.
 
  - **Afficher les maintenances :**  Affiche l'historique des maintenances effectuées sur les véhicules.
 
- **Afficher les statistiques :** 
  - Affiche le total des kilomètres parcourus par tous les véhicules.

  - Indique le chauffeur le plus actif en termes de kilomètres parcourus.

### Conseils d'utilisation 
 
- **Saisie des données :**  Suivez les instructions à l'écran et assurez-vous de saisir des données valides (par exemple, des nombres positifs pour les distances et les durées).
 
- **Gestion des exceptions :**  Certaines opérations, comme la suppression d'un véhicule assigné à un trajet actif, ne sont pas autorisées et génèrent des messages d'erreur.

## Auteur 

- Judikael NEDEV

## Notes supplémentaires 
 
- **Sauvegarde des données :**  Les données sont stockées dans une base de données **SQLite**  appelée `flotte.db` dans le répertoire du projet. Assurez-vous de bien appliquer les migrations avant d'exécuter l'application afin que les tables soient correctement créées.
 
- **Extensions possibles :**  
  - **Planification automatique :**  Implémenter un algorithme pour assigner automatiquement des véhicules et des chauffeurs aux trajets en fonction de la disponibilité et des compétences.
 
  - **Programmation asynchrone :**  Utiliser la programmation asynchrone pour simuler le déroulement des trajets en temps réel.
 
  - **Interface utilisateur améliorée :**  Développer une interface graphique pour rendre l'application plus conviviale.

