
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../chat_service.dart';
import '../models/user.dart';

class CreateChannelDialog extends StatefulWidget {
  const CreateChannelDialog({super.key});

  @override
  State<CreateChannelDialog> createState() => _CreateChannelDialogState();
}

class _CreateChannelDialogState extends State<CreateChannelDialog> {
  final _formKey = GlobalKey<FormState>();
  final _name = TextEditingController();

  @override
  Widget build(BuildContext context) {
    final user = Provider.of<User>(context);
    final chat = Provider.of<ChatService>(context);
    return AlertDialog(
      title: const Text('Add Channel'),
      content: Form(
        key: _formKey,
        child: TextFormField(
          controller: _name,
          decoration: const InputDecoration(label: Text('Name')),
          validator: (value) => value!.isEmpty ? 'Name required' : null,
        ),
      ),
      actions: [
        TextButton(
          child: const Text('Cancel'),
          onPressed: () {
            Navigator.of(context).pop();
          },
        ),
        TextButton(
          child: const Text('Add'),
          onPressed: () {
            if (!_formKey.currentState!.validate()) return;
            chat.createChannel(user, _name.value.text);
            Navigator.of(context).pop();
          },
        ),
      ],
    );
  }
}