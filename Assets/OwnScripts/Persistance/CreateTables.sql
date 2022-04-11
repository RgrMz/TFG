-- Tabler for players
CREATE TABLE IF NOT EXISTS Player (
	UserId INT,
	Age INT NOT NULL,
	PRIMARY KEY (UserId AUTOINCREMENT)
);

-- Table for badges
CERATE TABLE IF NOT EXISTS BADGE (
	BadgeId INT PRIMARY KEY,
	Description TEXT NOT NULL,
	Image BLOB NOT NULL,
	PRIMARY KEY (BadgeId AUTOINCREMENT)
);

-- Table for projects
CREATE TABLE IF NOT EXISTS Project (
	ProjectId INT,
	DESCRIPTION TEXT NOT NULL, 
	TYPE CHAR(3) NOT NULL, 
	PRIMARY KEY (ProjectId AUTOINCREMENT)
);

-- Table for objectives
CREATE TABLE IF NOT EXISTS Objective (
	ObjectiveId INT,
	DESCRIPTION TEXT NOT NULL, 
	NumberOfSteps INT NOT NULL,
	IsCompleted INT NOT NULL DEFAULT 0,
	CurentStep INT NOR NULL,
	TriggersPipeline INT NOT NULL DEFAULT 0,
	PRIMARY KEY (ObjectiveId AUTOINCREMENT)
);

-- Table for metrics
CREATE TABLE IF NOT EXISTS Metric (
	MetricId INT,
	Name NVARCHAR(30) NOT NULL, 
	Description TEXT NOT NULL,
	PRIMARY KEY (MetricId AUTOINCREMENT)
);

-- Table for effects
CREATE TABLE IF NOT EXISTS Effect (
	EffectId INT,
	Value REAL NOT NULL, 
	Indicator NVARCHAR(20) NOT NULL,
	-- Duration represents in-game days that lasts the effect
	Duration INT DAYS NOT NULL, 
	PRIMARY KEY (EffectId AUTOINCREMENT)
);

-- Table for problems appearing at a certain condition based on one or more indicators
CREATE TABLE IF NOT EXISTS Problem (
	ProblemId INT,
	Description TEXT NOT NULL,
	NumberOfSolutions SMALLINT NOT NULL,
	PRIMARY KEY (ProblemId AUTOINCREMENT
);

-- Table for practices
CREATE TABLE IF NOT EXISTS Practice (
	PracticeId INT,
	EffectId INT,
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
	AspectId INT,
	MetricId INT NOT NULL,
	Name NVARCHAR(60) NOT NULL,
	PRIMARY KEY (AspectId AUTOINCREMENT),
	FOREIGN KEY (MetricId) REFERENCES Metric (MetricId)
);

-- Table for properties of the project in the game: speed of charging interactions' bars, initial budget, ...
CREATE TABLE IF NOT EXISTS Property (
	PropertyId INT,
	ProjectId INT NOT NULL,
	Name NVARCHAR(60) NOT NULL,
	Value REAL NOT NULL,
	PRIMARY KEY (PropertyId AUTOINCREMENT),
	FOREIGN KEY (ProjectId) REFERENCES Project (ProjectId)
);

-- Table for each game played by players
CREATE TABLE IF NOT EXISTS Game (
	GameId INT,
	UserId INT NOT NULL,
	ProjectId INT NOT NULL,
	-- Rank based on CALMS indicators + W or L
	Result NVARCHAR(60) DEFAULT NULL,
	Difficulty NVARCHAR(15) NOT NULL,
	PRIMARY KEY (GameId AUTOINCREMENT),
	FOREIGN KEY (ProjectId) REFERENCES Project (ProjectId),
	FOREIGN KEY (UserId) REFERENCES User (UserId)
);

-- Table for each solution of each problem
CREATE TABLE IF NOT EXISTS Solution (
	SolutionId INT,
	EffectId INT NOT NULL,
	ProblemId INT NOT NULL,
	Cost REAL,
	Profit REAL,
	Description TEXT NOT NULL,
	PRIMARY KEY (SolutionId, EffectId),
	FOREIGN KEY (EffectId) REFERENCES Effect (EffectId),
	FOREIGN KEY (ProblemId) REFERENCES Problem (ProblemId)
);

-- Table for the relationship between Objective and Project
CREATE TABLE IF NOT EXISTS ObjectivesPerProject (
	ProjectId INT,
	ObjectiveId INT,
	PRIMARY KEY (ProjectId, ObjectiveId),
	FOREIGN KEY (ProjectId) REFERENCES Project (ProjectId),
	FOREIGN KEY (ObjectiveId) REFERENCES Objective (ObjectiveId)
);

-- Table for the relationship between MeasuredAspect and Game
CREATE TABLE IF NOT EXISTS MeasuredAspectPerGame (
	GameId INT,
	AspectId INT,
	Value REAL NOT NULL DEFAULT 0,
	PRIMARY KEY (GameId, AspectId),
	FOREIGN KEY (GameId) REFERENCES Game (GameId),
	FOREIGN KEY (AspectId) REFERENCES MeasuredAspect (AspectId)
);

-- Table for the relationship between Badges and Player
CREATE TABLE IF NOT EXISTS BadgesWon (
	UserId INT,
	BadgeId INT,
	PRIMARY KEY (UserId, BadgeId),
	FOREIGN KEY (UserId) REFERENCES User (UserId),
	FOREIGN KEY (BadgeId) REFERENCES Badge (BadgeId)
);