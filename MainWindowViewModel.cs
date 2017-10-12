﻿using System;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using CommentBoardRefine.Models;
using System.Collections.ObjectModel;


namespace CommentBoardRefine.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region 
        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */


#endregion

        private ObservableCollection<Column> _BindingModel { get; set; }

        public ObservableCollection<Column> BindingModel
        {
            get { return _BindingModel; }
            set { _BindingModel = value;
                RaisePropertyChanged("BindingModel");
            }
        }

        private DateTime _UpdateTime;
        public DateTime UpdateTime { get { return _UpdateTime; }
            set { _UpdateTime = value;
                RaisePropertyChanged("UpdateTime");
            } }


        public void Initialize()
        {
        }


    public    MainWindowViewModel()
        {
            BindingModel = new BindingClass().BindModel;
            UpdateTime = DateTime.Now;
            
        }


        #region Postボタン
        private ViewModelCommand _PostCommand;
        public ViewModelCommand PostCommand
        {
            get
            {
                if (_PostCommand == null)
                {
                    _PostCommand = new ViewModelCommand(Post);
                }

                return _PostCommand;
            }

        }
        public void Post()
        {
            MainWindowControlModel.CreatePostWindow(this);

        }

        #endregion

        #region Reloadボタン
        private ViewModelCommand _ReloadCommand;

        public ViewModelCommand ReloadCommand
        {
            get
            {
                if (_ReloadCommand == null)
                {
                    _ReloadCommand = new ViewModelCommand(Reload);
                }

                return _ReloadCommand;
            }

        }

        public void Reload()
        {
            BindingModel = new BindingClass().BindModel;
            UpdateTime = DateTime.Now;

        }
        #endregion
    }
}