-- Insert possible projects for the game
INSERT OR IGNORE INTO Project (Description, TYPE)
    VALUES ("This project consists of developing a web application for handling the time control of 
			 JuVerne's employees who work remotely. It will be developed with ReactJS and Java Spring", "Dev"),
		   ("This project consists of operating a web application for handling the time control of 
			 JuVerne's employees who work remotely. The software will be deployed to Amazon Web Service servers in the cloud, so it will
			 be monitored and operated thorugh services of this platform such as Amazon CloudFormation or Amazon CloudWatch", "Ops"),
		   ("This project consists of migrating the legacy code of the Information System of the restaurant GoodSushi. They know its Information System
			 has quality flaws and errors, so they want them to be solved. They also want it to be migrated from Java 2EE to Microsoft .NET", "Dev"),
		   ("This project consists of operating the new code developed by the development team based on the legacy one of the Information System of the restaurant GoodSushi. 
		     They know its Information System has quality flaws along with performance issues and mid-to-long downtimes. The software will be deployed to Amazon Web Service servers in the cloud, so it will
			 be monitored and operated thorugh services of this platform such as Amazon CloudFormation or Amazon CloudWatch ", "Ops");
			 