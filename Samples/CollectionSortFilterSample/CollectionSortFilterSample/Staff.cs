using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace CollectionSortFilterSample
{
    /// <summary>
    /// スタッフ情報
    /// </summary>
    public class Staff : BindableBase
    {
        private Role _role;

        /// <summary>役割</summary>
        public Role Role
        {
            get { return _role; }
            set { SetProperty<Role>(ref _role, value); }
        }

        private int _id;

        /// <summary>ID</summary>
        public int Id
        {
            get { return _id; }
            set { SetProperty<int>(ref _id, value); }
        }

        private string _name;

        /// <summary>名前</summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty<string>(ref _name, value); }
        }

        private int _age;

        /// <summary>年齢</summary>
        public int Age
        {
            get { return _age; }
            set { SetProperty<int>(ref _age, value); }
        }

    }
}
