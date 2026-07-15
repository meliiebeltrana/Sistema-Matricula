PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);
INSERT INTO __EFMigrationsHistory VALUES('00000000000000_CreateIdentitySchema','1.0.2');
INSERT INTO __EFMigrationsHistory VALUES('20260715030130_AgregarTablaCarreras','10.0.8');
INSERT INTO __EFMigrationsHistory VALUES('20260715034103_AgregarTablaCursos','10.0.8');
INSERT INTO __EFMigrationsHistory VALUES('20260715041137_AgregarDocentesEstudiantesMatriculas','10.0.8');
CREATE TABLE IF NOT EXISTS "AspNetRoles" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetRoles" PRIMARY KEY,
    "ConcurrencyStamp" TEXT,
    "Name" TEXT,
    "NormalizedName" TEXT
);
CREATE TABLE IF NOT EXISTS "AspNetUserTokens" (
    "UserId" TEXT NOT NULL,
    "LoginProvider" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Value" TEXT,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name")
);
CREATE TABLE IF NOT EXISTS "AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "AccessFailedCount" INTEGER NOT NULL,
    "ConcurrencyStamp" TEXT,
    "Email" TEXT,
    "EmailConfirmed" INTEGER NOT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT,
    "NormalizedEmail" TEXT,
    "NormalizedUserName" TEXT,
    "PasswordHash" TEXT,
    "PhoneNumber" TEXT,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "SecurityStamp" TEXT,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "UserName" TEXT
);
CREATE TABLE IF NOT EXISTS "AspNetRoleClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY AUTOINCREMENT,
    "ClaimType" TEXT,
    "ClaimValue" TEXT,
    "RoleId" TEXT NOT NULL,
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY AUTOINCREMENT,
    "ClaimType" TEXT,
    "ClaimValue" TEXT,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserLogins" (
    "LoginProvider" TEXT NOT NULL,
    "ProviderKey" TEXT NOT NULL,
    "ProviderDisplayName" TEXT,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "AspNetUserRoles" (
    "UserId" TEXT NOT NULL,
    "RoleId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "__EFMigrationsLock" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK___EFMigrationsLock" PRIMARY KEY,
    "Timestamp" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Carreras" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Carreras" PRIMARY KEY AUTOINCREMENT,
    "Nombre" TEXT NOT NULL,
    "Codigo" TEXT NOT NULL,
    "Descripcion" TEXT NULL,
    "Activa" INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS "Cursos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Cursos" PRIMARY KEY AUTOINCREMENT,
    "Codigo" TEXT NOT NULL,
    "Nombre" TEXT NOT NULL,
    "Creditos" INTEGER NOT NULL,
    "CupoMaximo" INTEGER NOT NULL,
    "Activo" INTEGER NOT NULL,
    "CarreraId" INTEGER NOT NULL,
    CONSTRAINT "FK_Cursos_Carreras_CarreraId" FOREIGN KEY ("CarreraId") REFERENCES "Carreras" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Docentes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Docentes" PRIMARY KEY AUTOINCREMENT,
    "Identificacion" TEXT NOT NULL,
    "NombreCompleto" TEXT NOT NULL,
    "Correo" TEXT NOT NULL,
    "Especialidad" TEXT NOT NULL,
    "Telefono" TEXT NULL,
    "Activo" INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS "Estudiantes" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Estudiantes" PRIMARY KEY AUTOINCREMENT,
    "Identificacion" TEXT NOT NULL,
    "NombreCompleto" TEXT NOT NULL,
    "Correo" TEXT NOT NULL,
    "Telefono" TEXT NULL,
    "FechaNacimiento" TEXT NOT NULL,
    "CarreraId" INTEGER NOT NULL,
    "Activo" INTEGER NOT NULL,
    CONSTRAINT "FK_Estudiantes_Carreras_CarreraId" FOREIGN KEY ("CarreraId") REFERENCES "Carreras" ("Id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "Matriculas" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Matriculas" PRIMARY KEY AUTOINCREMENT,
    "EstudianteId" INTEGER NOT NULL,
    "CursoId" INTEGER NOT NULL,
    "FechaMatricula" TEXT NOT NULL,
    "PeriodoAcademico" TEXT NOT NULL,
    "Estado" TEXT NOT NULL,
    "NotaFinal" TEXT NULL,
    CONSTRAINT "FK_Matriculas_Cursos_CursoId" FOREIGN KEY ("CursoId") REFERENCES "Cursos" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Matriculas_Estudiantes_EstudianteId" FOREIGN KEY ("EstudianteId") REFERENCES "Estudiantes" ("Id") ON DELETE CASCADE
);
CREATE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");
CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");
CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");
CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");
CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");
CREATE INDEX "IX_AspNetUserRoles_UserId" ON "AspNetUserRoles" ("UserId");
CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");
CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");
CREATE INDEX "IX_Cursos_CarreraId" ON "Cursos" ("CarreraId");
CREATE INDEX "IX_Estudiantes_CarreraId" ON "Estudiantes" ("CarreraId");
CREATE INDEX "IX_Matriculas_CursoId" ON "Matriculas" ("CursoId");
CREATE INDEX "IX_Matriculas_EstudianteId" ON "Matriculas" ("EstudianteId");
COMMIT;
