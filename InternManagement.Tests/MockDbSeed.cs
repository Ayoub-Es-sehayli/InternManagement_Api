using System;
using System.Collections.Generic;
using System.Linq;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.Tests
{
  public class MockDbSeed
  {
    public InternContext context { get; internal set; }
    private static bool initialized = false;

    public MockDbSeed(string DbName)
    {
      var options = new DbContextOptionsBuilder<InternContext>()
        .UseInMemoryDatabase(DbName)
        .Options;

      this.context = new InternContext(options);
      if (!initialized)
      {
        LoadDepartment();
        LoadInterns();
        initialized = true;
      }
    }

    private void LoadDepartment()
    {
      var locations = new List<Location>
      {
        new Location
        {
          Id = 1,
          Name = "Al Omrane Rabat-Salé-Kénitra"
        },
        new Location
        {
          Id = 2,
          Name = "Al Omrane Tamesna"
        }
      };
      context.Set<Location>().AddRange(locations);
      context.SaveChanges();
      var departments = new List<Department>
      {
        new Department { Id = 1, Name = "Direction Generale" , LocationId = 1},
        new Department { Id = 2, Name = "Charge de missions partenaires" , LocationId = 1 },
        new Department { Id = 3, Name = "Departement juridique" , LocationId = 1 },
        new Department { Id = 4, Name = "Division Qualite et developpement durable" , LocationId = 1 },
        new Department { Id = 5, Name = "Division Systeme d'Informations" , LocationId = 1 },
        new Department { Id = 6, Name = "Direction Technique et Ingenierie" , LocationId = 1 },
        new Department { Id = 7, Name = "Direction Commerciale et marketing" , LocationId = 1 },
        new Department { Id = 8, Name = "Direction Organisation et Capital Humain" , LocationId = 1 },
        new Department { Id = 9, Name = "Direction financiere et contrôle de gestion" , LocationId = 1 },
        new Department { Id = 10, Name = "Al Omrane Tamesna" , LocationId = 2 }
      };

      context.Departments.AddRange(departments);
      context.SaveChanges();
      var divisions = new List<Division>
      {
        new Division { Id = 1, Name = "Direction Generale", DepartmentId = 1 },
        new Division { Id = 2, Name = "Charge de missions partenaires", DepartmentId = 2 },
        new Division { Id = 3, Name = "Departement juridique", DepartmentId = 3 },
        new Division { Id = 4, Name = "Division Qualite et developpement durable", DepartmentId = 4 },
        new Division { Id = 5, Name = "Division Systeme d'Informations", DepartmentId = 5 },
        new Division { Id = 6, Name = "Direction Technique et Ingenierie", DepartmentId = 6 },
        new Division { Id = 7, Name = "Departement conception et developpement", DepartmentId = 6 },
        new Division { Id = 8, Name = "Division developpement  et affaires foncieres", DepartmentId = 6 },
        new Division { Id = 9, Name = "Division des etudes et Programmation", DepartmentId = 6 },
        new Division { Id = 10, Name = "Departement realisation", DepartmentId = 6 },
        new Division { Id = 11, Name = "Division des marches", DepartmentId = 6 },
        new Division { Id = 12, Name = "Division Suivi et contrôle de realisation", DepartmentId = 6 },
        new Division { Id = 13, Name = "Direction Commerciale et marketing", DepartmentId = 7 },
        new Division { Id = 14, Name = "Departement marketing et communication", DepartmentId = 7 },
        new Division { Id = 15, Name = "Division des etudes  marketing", DepartmentId = 7 },
        new Division { Id = 16, Name = "Division Communication Operationnelle", DepartmentId = 7 },
        new Division { Id = 17, Name = "Departement Animation reseau", DepartmentId = 7 },
        new Division { Id = 18, Name = "Division Administration des ventes", DepartmentId = 7 },
        new Division { Id = 19, Name = "Division Recouvrement et destockage", DepartmentId = 7 },
        new Division { Id = 20, Name = "Direction Organisation et Capital Humain", DepartmentId = 8 },
        new Division { Id = 21, Name = "Departement Ressources Humaines", DepartmentId = 8 },
        new Division { Id = 22, Name = "Division Gestion Administrative et paie", DepartmentId = 8 },
        new Division { Id = 23, Name = "Division Developpement RH et Formation", DepartmentId = 8 },
        new Division { Id = 24, Name = "Departement logistique et moyens  generaux", DepartmentId = 8 },
        new Division { Id = 25, Name = "Direction financiere et contrôle de gestion", DepartmentId = 9 },
        new Division { Id = 26, Name = "Departement financier et comptable", DepartmentId = 9 },
        new Division { Id = 27, Name = "Division Finance et tresorerie", DepartmentId = 9 },
        new Division { Id = 28, Name = "Division comptabilite et Fiscalite", DepartmentId = 9 },
        new Division { Id = 29, Name = "Departement contrôle de gestion", DepartmentId = 9 },
        new Division { Id = 30, Name = "Al Omrane Tamesna" , DepartmentId = 10 }
      };
      context.Divisions.AddRange(divisions);
      context.SaveChanges();
    }

    private void LoadInterns()
    {
      var today = DateTime.Today;
      var currentTime = new DateTime(2021, 8, 4, 8, 45, 00);


      var interns = new List<Intern>();
      for (int i = 1; i < 100; i++)
      {
        interns.Add(new Intern
        {
          Id = i,
          FirstName = "Mohamed",
          LastName = "Hariss",
          Email = "mohamed.hariss@gmail.com",
          Phone = "0684257139",
          AttendanceAlarmState = eAttendanceAlarmState.None,
          FileAlarmState = eFileAlarmState.None,
          DivisionId = 25,
          Gender = eGender.Male,
          StartDate = DateTime.Today,
          EndDate = DateTime.Today.AddMonths(2),
          State = eInternState.Started,
          Decision = new Decision
          {
            Id = i,
            Date = DateTime.Today,
            Code = "1447/2021"
          },
          Documents = new Documents
          {
            Id = i,
            CV = eDocumentState.Submitted,
            Letter = eDocumentState.Submitted,
            Insurance = eDocumentState.Submitted,
            Convention = eDocumentState.Submitted,
            Report = eDocumentState.Invalid,
            EvaluationForm = eDocumentState.Missing
          },
          Attendance = new List<Attendance>
          {
            new Attendance
            {
              Id = (i * 5) - 4,
              date = today,
              time = currentTime,
              Type = eAttendanceType.Absent
            },
            new Attendance
            {
              Id = (i * 5) - 3,
              date = today.AddDays(1),
              time = currentTime,
              Type = eAttendanceType.Absent
            },
            new Attendance
            {
              Id = (i * 5) - 2,
              date = today.AddDays(2),
              time = currentTime,
              Type = eAttendanceType.Absent
            },
            new Attendance
            {
              Id = (i * 5) - 1,
              date = today.AddDays(3),
              time = currentTime,
              Type = eAttendanceType.Absent
            },
            new Attendance
            {
              Id = (i * 5),
              date = today.AddDays(4),
              time = currentTime,
              Type = eAttendanceType.Absent
            },
          }
        });
      }
      context.Interns.AddRange(interns);
      context.SaveChanges();
    }

  }
}