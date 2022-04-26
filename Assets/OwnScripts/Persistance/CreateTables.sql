-- Tabler for players
CREATE TABLE IF NOT EXISTS Player (
	UserId INTEGER,
	UserName TEXT NOT NULL,
	Age INTEGER NOT NULL,
	PRIMARY KEY (UserId AUTOINCREMENT)
);

-- Table for badges
CREATE TABLE IF NOT EXISTS BADGE (
	BadgeId INTEGER,
	Description TEXT NOT NULL,
	Image BLOB NOT NULL,
	PRIMARY KEY (BadgeId AUTOINCREMENT)
);

-- Table for projects
CREATE TABLE IF NOT EXISTS Project (
	ProjectId INTEGER,
	DESCRIPTION TEXT NOT NULL, 
	TYPE CHAR(3) NOT NULL, 
	PRIMARY KEY (ProjectId AUTOINCREMENT)
);

-- Table for objectives
CREATE TABLE IF NOT EXISTS Objective (
	ObjectiveId INTEGER,
	DESCRIPTION TEXT NOT NULL, 
	NumberOfSteps INTEGER NOT NULL,
	IsCompleted INTEGER NOT NULL DEFAULT 0,
	TriggersPipeline INTEGER NOT NULL DEFAULT 0,
	Type TEXT NOT NULL,
	PRIMARY KEY (ObjectiveId AUTOINCREMENT)
);

-- Table for metrics
CREATE TABLE IF NOT EXISTS Metric (
	MetricId INTEGER,
	Name NVARCHAR(30) NOT NULL, 
	Description TEXT NOT NULL,
	PRIMARY KEY (MetricId AUTOINCREMENT)
);

-- Table for effects
CREATE TABLE IF NOT EXISTS Effect (
	EffectId INTEGER,
	Value REAL NOT NULL, 
	Indicator NVARCHAR(20) NOT NULL,
	-- Duration represents in-game days that lasts the effect
	Duration INTEGER DAYS NOT NULL, 
	PRIMARY KEY (EffectId AUTOINCREMENT)
);

-- Table for problems appearing at a certain condition based on one or more indicators
CREATE TABLE IF NOT EXISTS Problem (
	ProblemId INTEGER,
	Description TEXT NOT NULL,
	NumberOfSolutions SMALLINTEGER NOT NULL,
	PRIMARY KEY (ProblemId AUTOINCREMENT)
);

-- Table for practices
CREATE TABLE IF NOT EXISTS Practice (
	PracticeId INTEGER,
	EffectId INTEGER,
	PracticeName NVARCHAR(60) NOT NULL,
	Cost REAL NOT NULL,
	Description TEXT NOT NULL,
	Profit REAL,
	ToolImage BLOB NOT NULL,
	PRIMARY KEY (PracticeId, EffectId),
	FOREIGN KEY (EffectId) REFERENCES Effect (EffectId)
);

-- Table for measured aspects for calculating metrics
CREATE TABLE IF NOT EXISTS MeasuredAspect (
	AspectId INTEGER,
	MetricId INTEGER NOT NULL,
	Name NVARCHAR(60) NOT NULL,
	PRIMARY KEY (AspectId AUTOINCREMENT),
	FOREIGN KEY (MetricId) REFERENCES Metric (MetricId)
);

-- Table for properties of the project in the game: speed of charging INTEGEReractions' bars, initial budget, ...
CREATE TABLE IF NOT EXISTS Property (
	PropertyId INTEGER,
	Difficulty VARCHAR(15) NOT NULL,
	Name NVARCHAR(60) NOT NULL,
	Value REAL NOT NULL,
	PRIMARY KEY (PropertyId AUTOINCREMENT)
);

-- Table for each game played by players
CREATE TABLE IF NOT EXISTS Game (
	GameId INTEGER,
	UserId INTEGER NOT NULL,
	ProjectId INTEGER NOT NULL,
	-- Rank based on CALMS indicators + W or L
	Result NVARCHAR(60) DEFAULT NULL,
	Difficulty NVARCHAR(15) NOT NULL,
	PRIMARY KEY (GameId AUTOINCREMENT),
	FOREIGN KEY (ProjectId) REFERENCES Project (ProjectId),
	FOREIGN KEY (UserId) REFERENCES User (UserId)
);

-- Table for each solution of each problem
CREATE TABLE IF NOT EXISTS Solution (
	SolutionId INTEGER,
	EffectId INTEGER NOT NULL,
	ProblemId INTEGER NOT NULL,
	Cost REAL,
	Profit REAL,
	Description TEXT NOT NULL,
	PRIMARY KEY (SolutionId, EffectId),
	FOREIGN KEY (EffectId) REFERENCES Effect (EffectId),
	FOREIGN KEY (ProblemId) REFERENCES Problem (ProblemId)
);

-- Table for the relationship between Objective and Project
CREATE TABLE IF NOT EXISTS ObjectivesPerProject (
	ProjectId INTEGER,
	ObjectiveId INTEGER,
	OrderInProject INTEGER,
	PRIMARY KEY (ProjectId, ObjectiveId, OrderInProject),
	FOREIGN KEY (ProjectId) REFERENCES Project (ProjectId),
	FOREIGN KEY (ObjectiveId) REFERENCES Objective (ObjectiveId)
);

-- Table for thoseo objectives that have an impact in the CALMS indicators
CREATE TABLE IF NOT EXISTS EffectPerObjective(
	ObjectiveId INTEGER,
	EffectId INTEGER,
	PRIMARY KEY (SolutionId, EffectId),
	FOREIGN KEY (EffectId) REFERENCES Effect (EffectId)
);

-- Table for the properties of each project
CREATE TABLE IF NOT EXISTS PropertyPerProject (
	PropertyId INTEGER,
	ProjectId INTEGER,
	PRIMARY KEY (PropertyId, ProjectId),
	FOREIGN KEY (ProjectId) REFERENCES Project (ProjectId)
);

-- Table for the relationship between MeasuredAspect and Game
CREATE TABLE IF NOT EXISTS MeasuredAspectPerGame (
	GameId INTEGER,
	AspectId INTEGER,
	Value REAL NOT NULL DEFAULT 0,
	PRIMARY KEY (GameId, AspectId),
	FOREIGN KEY (GameId) REFERENCES Game (GameId),
	FOREIGN KEY (AspectId) REFERENCES MeasuredAspect (AspectId)
);

-- Table for the relationship between Badges and Player
CREATE TABLE IF NOT EXISTS BadgesWon (
	UserId INTEGER,
	BadgeId INTEGER,
	PRIMARY KEY (UserId, BadgeId),
	FOREIGN KEY (UserId) REFERENCES User (UserId),
	FOREIGN KEY (BadgeId) REFERENCES Badge (BadgeId)
);