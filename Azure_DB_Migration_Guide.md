# Step-by-Step Guide: Migrating a SQL Server Database to Azure SQL

This guide provides a detailed plan to migrate an existing local SQL Server database to a new Azure SQL Database using the deployment wizard in SQL Server Management Studio (SSMS).

### **Part 1: Prerequisites**

1.  **Install SQL Server Management Studio (SSMS):** If you don't already have it, [download and install the latest version of SSMS](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms).
2.  **Identify Your Local Database:** Know the name of your local SQL Server instance (e.g., `(localdb)\MSSQLLocalDB`, `.\SQLEXPRESS`) and the name of the database you intend to migrate.
3.  **Azure Subscription:** Ensure you have an active Azure subscription.

---

### **Part 2: Create and Configure the Target Azure SQL Database**

1.  **Create Azure SQL Database:**
    *   Sign in to the **Azure Portal**.
    *   Search for and select **"SQL databases"** and click **"Create"**.
    *   Follow the prompts to create a new SQL Server and a new database. Choose a compute/storage tier that fits your needs (you can start with a basic or standard tier and scale later).
    *   **Important:** Securely store the server admin login and password you create.

2.  **Configure Firewall Rules:**
    *   After the database is deployed, navigate to its resource page in the Azure Portal.
    *   Go to the **"Networking"** section (or find "Set server firewall").
    *   Enable the **"Allow Azure services and resources to access this server"** option.
    *   Click **"Add your current client IP address"**. This is essential as it allows SSMS on your machine to connect to and migrate data to the Azure database.
    *   Click **Save**.

3.  **Get the Connection String:**
    *   Go to the **"Connection strings"** section of your Azure SQL database.
    *   Copy the **ADO.NET** connection string and save it in a text editor. You will need this later. Remember to replace the password placeholder `{your_password}` with your actual password.

---

### **Part 3: Migrate the Database using SSMS**

1.  **Connect to Your Local Database:**
    *   Open **SSMS**.
    *   In the "Connect to Server" dialog, enter the name of your **local** SQL Server instance and connect using your credentials (usually "Windows Authentication").

2.  **Launch the Deployment Wizard:**
    *   In the **Object Explorer** panel on the left, expand the "Databases" folder.
    *   Right-click on your local SimplCommerce database.
    *   Select **Tasks > Deploy Database to Microsoft Azure SQL Database...**.

3.  **Follow the Wizard:**
    *   **Introduction:** Click "Next".
    *   **Deployment Settings:**
        *   Click the **"Connect"** button to specify the target server.
        *   In the popup, enter the **Azure SQL Server name** you created (e.g., `your-server.database.windows.net`), choose "SQL Server Authentication", and enter the admin login and password for your Azure database. Click **Connect**.
        *   The wizard will connect to your Azure server. You can now set the name for the new database on Azure and choose the service objective (e.g., Basic, S1, S2).
        *   Click **Next**.
    *   **Summary:** The wizard will show a summary of the actions it will perform. Review them and click **Finish**.

4.  **Monitor Progress:**
    *   The wizard will now assess your local database, create the schema on Azure, and transfer all the data. This process can take some time depending on the size of your database.
    *   If the operation completes successfully, your database has been migrated.

---

### **Part 4: Update Your Application Configuration**

The final step is to tell your SimplCommerce application to use the new database in Azure.

1.  **Retrieve the Azure Connection String:** Get the ADO.NET connection string you saved in Part 2.
2.  **Update Configuration:**
    *   **For your deployed Azure App Service:** Go to your App Service in the Azure Portal, navigate to **Configuration > Connection Strings**, and update the `DefaultConnection` value to your new Azure SQL connection string.
    *   **For your local development environment:** Open the `appsettings.Development.json` file in the `SimplCommerce.WebHost` project and update the `DefaultConnection` value there. This allows you to run the application locally while connected to the database in Azure.

After updating the connection string, restart your application. It will now be connected to the migrated database in Azure.
