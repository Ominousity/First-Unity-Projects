import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../chat_service.dart';
import '../models/user.dart';
import '../widgets/create_channel_button.dart';
import '../models/channel.dart';
import '../widgets/logout_button.dart';
import '../widgets/uid_button.dart';
import 'messages_screen.dart';

class ChannelsScreen extends StatelessWidget {
  const ChannelsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final user = Provider.of<User?>(context);
    final chat = Provider.of<ChatService>(context);
    if(user == null) return Container();
    return Scaffold(
      appBar: AppBar(
          title: const Text('Channels'),
          actions: const [CreateChannelButton(), UidButton(), LogoutButton()]),
      body: StreamBuilder(
        stream: chat.channels(user),
        builder: (context, snapshot) => ListView(children: [
          if (snapshot.hasData) ...snapshot.data!.map((e) => ChannelTile(e))
        ]),
      ),
    );
  }
}

class ChannelTile extends StatelessWidget {
  final Channel channel;
  const ChannelTile(this.channel, {super.key});

  @override
  Widget build(BuildContext context) {
    return ListTile(
      title: Text(channel.name),
      trailing: Text('Members: ${channel.members.length}'),
      onTap: () => Navigator.of(context).push(MaterialPageRoute(
        builder: (context) => MessagesScreen(channel: channel),
      )),
    );
  }
}
