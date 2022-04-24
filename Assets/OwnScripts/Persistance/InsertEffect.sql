INSERT OR REPLACE INTO Effect (Value, Indicator, Duration)
VALUES
	(10, "Culture", 10),
	(10, "Sharing", 10),
	(10, "Measurement", 10),
	(10, "Lean", 10),
	(10, "Automation", 10),
	(-10, "Culture", 10),
	(-10, "Sharing", 10),
	(-10, "Measurement", 10),
	(-10, "Lean", 10),
	(-10, "Automation", 10),
	(10, "Culture", 20),
	(10, "Sharing", 20),
	(10, "Measurement", 20),
	(10, "Lean", 20),
	(10, "Automation", 20),
	(-10, "Culture", 20),
	(-10, "Sharing", 20),
	(-10, "Measurement", 20),
	(-10, "Lean", 20),
	(-10, "Automation", 20);
	
-- Effects for those objectives that contributes to the sucessful
-- adoption of DevOps. Negative effect if some time has elapsed
-- and the objective has not been completed
INSERT OR REPLACE INTO Effect (Value, Indicator, Duration)
VALUES
	(0.6, "Culture", 3),
	(0.6, "Sharing", 3),
	(0.6, "Measurement", 3),
	(0.6, "Lean", 3),
	(0.6, "Automation", 3),
	(-0.6, "Culture", 3),
	(-0.6, "Sharing", 3),
	(-0.6, "Measurement", 3),
	(-0.6, "Lean", 3),
	(-0.6, "Automation", 3);