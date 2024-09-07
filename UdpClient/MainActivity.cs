using System.Net;
using System.Text;

namespace UdpClient
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView debug_, ip_, wgListenPort, wgPORT;
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            ip_ = FindViewById<TextView>(Resource.Id.ipcfg);
            wgListenPort = FindViewById<TextView>(Resource.Id.wgListenPort);


            wgPORT = FindViewById<TextView>(Resource.Id.wgPORT);
            Button b_send = FindViewById<Button>(Resource.Id.b_send);


            debug_ = FindViewById<TextView>(Resource.Id.debugstatus);

            if (ip_ != null && wgListenPort != null && b_send != null)
            {
                ip_.TextChanged += Ip__TextChanged;
                wgListenPort.TextChanged += Ip__TextChanged;

                b_send.Click += B_send_Click;
            }

        }

        private async void B_send_Click(object? sender, EventArgs e)
        {

            IPAddress ip;
            if (IPAddress.TryParse(ip_.Text, out ip))
            {
                if (int.TryParse(wgListenPort.Text, out int wgListen_Port) && int.TryParse(wgPORT.Text, out int wgPORT_port))
                {
                    System.Net.IPEndPoint p = new System.Net.IPEndPoint(ip.Address, wgPORT_port);

                    debug_.Text = "";

                    using (System.Net.Sockets.UdpClient udp = new System.Net.Sockets.UdpClient(wgListen_Port))
                    {

                        byte[] buffer = Encoding.ASCII.GetBytes(":)");

                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                udp.Send(buffer, buffer.Length, p);


                                debug_.Text += $"{DateTime.Now.ToLongTimeString()} {string.Join("-", buffer)} | {buffer.Length} \n";
                            }
                            catch (Exception de)
                            {
                                debug_.Text += de.ToString() + "\n";


                            }

                        }




                    }


                }



            }




        }

        private void Ip__TextChanged(object? sender, Android.Text.TextChangedEventArgs e)
        {
            TextView textView = (TextView)sender;
            if (debug_ != null)
                debug_.Text = textView?.Text ?? "null";
        }
    }
}
