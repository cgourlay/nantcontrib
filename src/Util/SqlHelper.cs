//
// NAntContrib
// Copyright (C) 2002 Tomas Restrepo (tomasr@mvps.org)
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307 USA
//

using System;
using System.Data;
using System.Data.OleDb;

namespace NAnt.Contrib.Util
{ 

   /// <summary>
   /// Helper class used to execute Sql Statements.
   /// </summary>
   public class SqlHelper
   {
      private OleDbConnection _connection;
      private OleDbTransaction _transaction;
      
      /// <summary>
      /// Initializes a new instance.
      /// </summary>
      /// <param name="connectionString">OleDB Connection string</param>
      /// <param name="useTransaction">True if you want to use a transaction</param>
      public SqlHelper(string connectionString, bool useTransaction)
      {
         _connection = new OleDbConnection(connectionString);
         _connection.Open();
         
         if ( useTransaction ) {
            _transaction = _connection.BeginTransaction();
         }
      }

      /// <summary>
      /// Close the connection and terminate
      /// </summary>
      /// <param name="commit">true if the transaction should be commited</param>
      public void Close(bool commit)
      {
         if ( _transaction != null ) {
            if ( commit ) {
               _transaction.Commit();
            } else {
               _transaction.Rollback();
            }
         }

         _connection.Close();
      }


      /// <summary>
      /// Executes a SQL statement.
      /// </summary>
      /// <param name="sql">SQL statement to execute</param>
      /// <returns>Data reader used to check the result</returns>
      public IDataReader Execute(string sql)
      {
         OleDbCommand command = new OleDbCommand(sql, _connection);
         return command.ExecuteReader();
      }



   } // class SqlHelper

} // namespace NAnt.Contrib.Util