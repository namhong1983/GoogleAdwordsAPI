using Google.Api.Ads.AdWords.v201802;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleAPICore.Persistence
{
    public class ManagedCustomerTreeNode
    {
        /// <summary>
        /// The parent node.
        /// </summary>
        private ManagedCustomerTreeNode parentNode;

        /// <summary>
        /// The account associated with this node.
        /// </summary>
        private ManagedCustomer account;

        /// <summary>
        /// The list of child accounts.
        /// </summary>
        private List<ManagedCustomerTreeNode> childAccounts = new List<ManagedCustomerTreeNode>();

        /// <summary>
        /// Gets or sets the parent node.
        /// </summary>
        public ManagedCustomerTreeNode ParentNode
        {
            get { return parentNode; }
            set { parentNode = value; }
        }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        public ManagedCustomer Account
        {
            get { return account; }
            set { account = value; }
        }

        /// <summary>
        /// Gets the child accounts.
        /// </summary>
        public List<ManagedCustomerTreeNode> ChildAccounts
        {
            get { return childAccounts; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            return String.Format("{0}, {1}", account.customerId, account.name);
        }

        /// <summary>
        /// Returns a string representation of the current level of the tree and
        /// recursively returns the string representation of the levels below it.
        /// </summary>
        /// <param name="depth">The depth of the node.</param>
        /// <param name="sb">The String Builder containing the tree
        /// representation.</param>
        /// <returns>The tree string representation.</returns>
        public StringBuilder ToTreeString(int depth, StringBuilder sb)
        {
            sb.Append('-', depth * 2);
            sb.Append(this);
            sb.AppendLine();
            foreach (ManagedCustomerTreeNode childAccount in childAccounts)
            {
                childAccount.ToTreeString(depth + 1, sb);
            }
            return sb;
        }
    }
}
