import 'package:flutter/material.dart';

import '../models/channel.dart';

class AddMemberDialog extends StatelessWidget {
  const AddMemberDialog({
    super.key,
    required this.channel,
  });

  final Channel channel;

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: const Text('Add User'),
      content: SizedBox(
        width: 300,
        height: 300,
        child: Text("TODO")
      ),
      actions: [
        TextButton(
          child: const Text('Cancel'),
          onPressed: () {
            Navigator.of(context).pop();
          },
        ),
      ],
    );
  }
}
