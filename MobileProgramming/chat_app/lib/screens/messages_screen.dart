import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../chat_service.dart';
import '../models/user.dart';
import '../utils.dart';
import '../models/channel.dart';
import '../models/message.dart';
import '../widgets/add_member_button.dart';

class MessagesScreen extends StatelessWidget {
  final padding = 8.0;
  final Channel channel;
  const MessagesScreen({required this.channel, super.key});

  @override
  Widget build(BuildContext context) {
    final chat = Provider.of<ChatService>(context);
    return Scaffold(
      appBar: AppBar(
        title: Text(channel.name),
        actions: [AddMemberButton(channel: channel)],
      ),
      body: Stack(
        children: [
          StreamBuilder(
              stream: chat.messages(channel),
              initialData: [],
              builder: (context, snapshot) {
                final messages = snapshot.data ?? [];
                return ListView(
                    children: messages
                        .map(
                          (message) => Column(children: [
                            sender(message),
                            textBubble(message),
                            SizedBox(height: padding * 2),
                          ]),
                        )
                        .toList());
              }),
          Align(alignment: Alignment.bottomCenter, child: messageInput(context))
        ],
      ),
    );
  }

  Widget textBubble(Message message) {
    return SizedBox(
      width: double.infinity,
      child: Card(
        child: Padding(
          padding: EdgeInsets.all(padding),
          child: Text(message.content),
        ),
      ),
    );
  }

  Widget sender(Message message) {
    return Padding(
      padding: EdgeInsets.symmetric(horizontal: padding),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        mainAxisSize: MainAxisSize.max,
        children: [
          Text(senderName(message.from)),
          if (message.timestamp != null) Text(since(message.timestamp!))
        ],
      ),
    );
  }

  Widget messageInput(BuildContext context) {
    final user = Provider.of<User>(context);
    final chat = Provider.of<ChatService>(context);
    return Padding(
      padding: EdgeInsets.all(padding),
      child: TextField(
        keyboardType: TextInputType.text,
        onSubmitted: (value) {
          if (value.isEmpty) return;
          chat.sendMessage(user, channel, value);
        },
        decoration: const InputDecoration(
          border: OutlineInputBorder(),
          suffixIcon: Icon(Icons.send),
        ),
      ),
    );
  }
}
